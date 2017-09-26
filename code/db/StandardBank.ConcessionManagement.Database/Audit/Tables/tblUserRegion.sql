CREATE TABLE [Audit].[tblUserRegion] (
    [pkAuditUserRegionId] INT          IDENTITY (1, 1) NOT NULL,
    [pkUserRegionId]      INT          NOT NULL,
    [fkAuditTypeId]       INT          NOT NULL,
    [Entity]              XML          NOT NULL,
    [Username]            VARCHAR (50) NOT NULL,
    [DateStamp]           DATETIME     CONSTRAINT [DF_Audit_tblUserRegion_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblUserRegion] PRIMARY KEY CLUSTERED ([pkAuditUserRegionId] ASC),
    CONSTRAINT [FK_Audit_tblUserRegion_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

