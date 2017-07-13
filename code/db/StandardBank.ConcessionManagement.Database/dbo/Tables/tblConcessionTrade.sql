CREATE TABLE [dbo].[tblConcessionTrade] (
    [pkConcessionTradeId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]      INT             NULL,
    [fkTransactionTypeId] INT             NULL,
    [fkChannelTypeId]     INT             NULL,
    [TableNumber]         INT             NULL,
    [TransactionVolume]   INT             NULL,
    [TransactionValue]    DECIMAL (18, 2) NULL,
    [fkBaseRateId]        INT             NULL,
    [AdValorem]           DECIMAL (18)    NULL,
    CONSTRAINT [PK_tblConcessionTrade] PRIMARY KEY CLUSTERED ([pkConcessionTradeId] ASC),
    CONSTRAINT [FK_tblConcessionTrade_rtblBaseRate] FOREIGN KEY ([fkBaseRateId]) REFERENCES [dbo].[rtblBaseRate] ([pkBaseRateId]),
    CONSTRAINT [FK_tblConcessionTrade_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId]),
    CONSTRAINT [FK_tblConcessionTrade_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblConcessionTrade_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionTrade_tblConcession1] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

