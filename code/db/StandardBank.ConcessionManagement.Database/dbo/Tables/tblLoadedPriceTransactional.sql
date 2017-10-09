CREATE TABLE [dbo].[tblLoadedPriceTransactional] (
    [pkLoadedPriceTransactionalId] INT IDENTITY (1, 1) NOT NULL,
    [fkTransactionTypeId]          INT NOT NULL,
    [fkLegalEntityAccountId]       INT NOT NULL,
    [fkTransactionTableNumberId]   INT NOT NULL,
    CONSTRAINT [PK_tblLoadedPriceTransactional] PRIMARY KEY CLUSTERED ([pkLoadedPriceTransactionalId] ASC),
    CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTransactionTableNumber] FOREIGN KEY ([fkTransactionTableNumberId]) REFERENCES [dbo].[rtblTransactionTableNumber] ([pkTransactionTableNumberId]),
    CONSTRAINT [FK_tblLoadedPriceTransactional_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblLoadedPriceTransactional_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);



