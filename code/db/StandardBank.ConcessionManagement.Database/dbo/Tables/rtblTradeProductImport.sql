CREATE TABLE [dbo].[rtblTradeProductImport] (
    [pkTradeProductImportId] INT          IDENTITY (1, 1) NOT NULL,
    [fkTradeProductId]       INT          NULL,
    [ImportFileChargeCode]   VARCHAR (50) NULL,
    CONSTRAINT [PK_rtblTradeProductImport] PRIMARY KEY CLUSTERED ([pkTradeProductImportId] ASC),
    CONSTRAINT [FK_rtblTradeProductImport_rtblTradeProduct] FOREIGN KEY ([fkTradeProductId]) REFERENCES [dbo].[rtblTradeProduct] ([pkTradeProductId])
);

