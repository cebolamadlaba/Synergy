CREATE TABLE [dbo].[tblLoadedPriceLending] (
    [pkLoadedPriceLendingId] INT             IDENTITY (1, 1) NOT NULL,
    [fkProductTypeId]        INT             NOT NULL,
    [fkLegalEntityAccountId] INT             NOT NULL,
    [MarginToPrime]          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_tblLoadedPriceLending] PRIMARY KEY CLUSTERED ([pkLoadedPriceLendingId] ASC),
    CONSTRAINT [FK_tblLoadedPriceLending_rtblProduct] FOREIGN KEY ([fkProductTypeId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblLoadedPriceLending_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

