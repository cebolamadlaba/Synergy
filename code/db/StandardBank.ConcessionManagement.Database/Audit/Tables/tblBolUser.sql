CREATE TABLE [Audit].[tblBolUser] (
    [pkAuditBolUserId] INT          IDENTITY (1, 1) NOT NULL,
    [pkBolUserId]      INT          NOT NULL,
    [fkAuditTypeId]    INT          NOT NULL,
    [Entity]           XML          NOT NULL,
    [Username]         VARCHAR (50) NOT NULL,
    [DateStamp]        DATETIME     CONSTRAINT [DF_Audit_tblBolUser_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblBolUser] PRIMARY KEY CLUSTERED ([pkAuditBolUserId] ASC),
    CONSTRAINT [FK_Audit_tblBolUser_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

