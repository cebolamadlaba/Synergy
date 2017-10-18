CREATE TABLE [dbo].[tblLoadedPriceCash] (
    [pkLoadedPriceCashId]    INT IDENTITY (1, 1) NOT NULL,
    [fkChannelTypeId]        INT NOT NULL,
    [fkLegalEntityAccountId] INT NOT NULL,
    [fkTableNumberId]        INT NOT NULL,
    CONSTRAINT [PK_tblLoadedPriceCash] PRIMARY KEY CLUSTERED ([pkLoadedPriceCashId] ASC),
    CONSTRAINT [FK_tblLoadedPriceCash_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId]),
    CONSTRAINT [FK_tblLoadedPriceCash_rtblTableNumber] FOREIGN KEY ([fkTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblLoadedPriceCash_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

