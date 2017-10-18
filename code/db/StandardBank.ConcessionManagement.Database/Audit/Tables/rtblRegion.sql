CREATE TABLE [Audit].[rtblRegion] (
    [pkAuditRegionId] INT          IDENTITY (1, 1) NOT NULL,
    [pkRegionId]      INT          NOT NULL,
    [fkAuditTypeId]   INT          NOT NULL,
    [Entity]          XML          NOT NULL,
    [Username]        VARCHAR (50) NOT NULL,
    [DateStamp]       DATETIME     CONSTRAINT [DF_Audit_rtblRegion_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblRegion] PRIMARY KEY CLUSTERED ([pkAuditRegionId] ASC),
    CONSTRAINT [FK_Audit_rtblRegion_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

