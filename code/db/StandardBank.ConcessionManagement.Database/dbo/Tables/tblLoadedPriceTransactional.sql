CREATE TABLE [dbo].[tblLoadedPriceTransactional] (
    [pkLoadedPriceTransactionalId] INT IDENTITY (1, 1) NOT NULL,
    [fkTransactionTypeId]          INT NOT NULL,
    [fkLegalEntityAccountId]       INT NOT NULL,
    [fkTableNumberId]              INT NOT NULL,
    CONSTRAINT [PK_tblLoadedPriceTransactional] PRIMARY KEY CLUSTERED ([pkLoadedPriceTransactionalId] ASC),
    CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTableNumber] FOREIGN KEY ([fkTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblLoadedPriceTransactional_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

