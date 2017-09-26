CREATE TABLE [Audit].[tblUserRole] (
    [pkAuditUserRoleId] INT          IDENTITY (1, 1) NOT NULL,
    [pkUserRoleId]      INT          NOT NULL,
    [fkAuditTypeId]     INT          NOT NULL,
    [Entity]            XML          NOT NULL,
    [Username]          VARCHAR (50) NOT NULL,
    [DateStamp]         DATETIME     CONSTRAINT [DF_Audit_tblUserRole_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblUserRole] PRIMARY KEY CLUSTERED ([pkAuditUserRoleId] ASC),
    CONSTRAINT [FK_Audit_tblUserRole_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

