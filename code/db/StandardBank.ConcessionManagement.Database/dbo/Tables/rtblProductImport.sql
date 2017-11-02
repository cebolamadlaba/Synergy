CREATE TABLE [dbo].[rtblProductImport] (
    [pkProductImportId] INT          IDENTITY (1, 1) NOT NULL,
    [fkProductId]       INT          NOT NULL,
    [ImportFileChannel] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_rtblProductImport] PRIMARY KEY CLUSTERED ([pkProductImportId] ASC),
    CONSTRAINT [FK_rtblProductImport_rtblProduct] FOREIGN KEY ([fkProductId]) REFERENCES [dbo].[rtblProduct] ([pkProductId])
);

