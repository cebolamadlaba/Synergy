CREATE TABLE [Audit].[rtblProvince] (
    [pkAuditProvinceId] INT          IDENTITY (1, 1) NOT NULL,
    [pkProvinceId]      INT          NOT NULL,
    [fkAuditTypeId]     INT          NOT NULL,
    [Entity]            XML          NOT NULL,
    [Username]          VARCHAR (50) NOT NULL,
    [DateStamp]         DATETIME     CONSTRAINT [DF_Audit_rtblProvince_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblProvince] PRIMARY KEY CLUSTERED ([pkAuditProvinceId] ASC),
    CONSTRAINT [FK_Audit_rtblProvince_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

