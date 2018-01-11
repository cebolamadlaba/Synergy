CREATE TABLE [Audit].[tblAccountExecutiveAssistant] (
    [pkAuditAccountExecutiveAssistantId] INT          IDENTITY (1, 1) NOT NULL,
    [pkAccountExecutiveAssistantId]      INT          NOT NULL,
    [fkAuditTypeId]                      INT          NOT NULL,
    [Entity]                             XML          NOT NULL,
    [Username]                           VARCHAR (50) NOT NULL,
    [DateStamp]                          DATETIME     CONSTRAINT [DF_Audit_tblAccountExecutiveAssistant_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblAccountExecutiveAssistant] PRIMARY KEY CLUSTERED ([pkAuditAccountExecutiveAssistantId] ASC),
    CONSTRAINT [FK_Audit_tblAccountExecutiveAssistant_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

