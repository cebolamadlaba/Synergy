CREATE TABLE [Audit].[AuditType] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuditType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

