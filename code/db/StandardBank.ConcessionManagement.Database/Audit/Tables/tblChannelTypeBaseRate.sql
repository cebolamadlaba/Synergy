CREATE TABLE [Audit].[tblChannelTypeBaseRate] (
    [pkAuditChannelTypeBaseRateId] INT          IDENTITY (1, 1) NOT NULL,
    [pkChannelTypeBaseRateId]      INT          NOT NULL,
    [fkAuditTypeId]                INT          NOT NULL,
    [Entity]                       XML          NOT NULL,
    [Username]                     VARCHAR (50) NOT NULL,
    [DateStamp]                    DATETIME     CONSTRAINT [DF_Audit_tblChannelTypeBaseRate_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_tblChannelTypeBaseRate] PRIMARY KEY CLUSTERED ([pkAuditChannelTypeBaseRateId] ASC),
    CONSTRAINT [FK_Audit_tblChannelTypeBaseRate_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

