CREATE TABLE [dbo].[rtblTransactionTypeImport] (
    [pkTransactionTypeImportId] INT          IDENTITY (1, 1) NOT NULL,
    [fkTransactionTypeId]       INT          NOT NULL,
    [ImportFileChannel]         VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_rtblTransactionTypeImport] PRIMARY KEY CLUSTERED ([pkTransactionTypeImportId] ASC),
    CONSTRAINT [FK_rtblTransactionTypeImport_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId])
);

