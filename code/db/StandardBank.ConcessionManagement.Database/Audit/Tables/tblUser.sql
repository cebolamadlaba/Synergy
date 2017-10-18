CREATE TABLE [Audit].[tblUser] (
    [pkAuditUserId] INT          IDENTITY (1, 1) NOT NULL,
    [pkUserId]      INT          NOT NULL,
    [fkAuditTypeId] INT          NOT NULL,
    [Entity]        XML          NOT NULL,
    [Username]      VARCHAR (50) NOT NULL,
    [DateStamp]     DATETIME     CONSTRAINT [DF_Audit_tblUser_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblUser] PRIMARY KEY CLUSTERED ([pkAuditUserId] ASC),
    CONSTRAINT [FK_Audit_tblUser_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

