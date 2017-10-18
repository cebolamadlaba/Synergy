CREATE TABLE [Audit].[rtblChannelType] (
    [pkAuditChannelTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkChannelTypeId]      INT          NOT NULL,
    [fkAuditTypeId]        INT          NOT NULL,
    [Entity]               XML          NOT NULL,
    [Username]             VARCHAR (50) NOT NULL,
    [DateStamp]            DATETIME     CONSTRAINT [DF_Audit_rtblChannelType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblChannelType] PRIMARY KEY CLUSTERED ([pkAuditChannelTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblChannelType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

