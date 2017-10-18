CREATE TABLE [Audit].[rtblProduct] (
    [pkAuditProductId] INT          IDENTITY (1, 1) NOT NULL,
    [pkProductId]      INT          NOT NULL,
    [fkAuditTypeId]    INT          NOT NULL,
    [Entity]           XML          NOT NULL,
    [Username]         VARCHAR (50) NOT NULL,
    [DateStamp]        DATETIME     CONSTRAINT [DF_Audit_rtblProduct_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblProduct] PRIMARY KEY CLUSTERED ([pkAuditProductId] ASC),
    CONSTRAINT [FK_Audit_rtblProduct_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

