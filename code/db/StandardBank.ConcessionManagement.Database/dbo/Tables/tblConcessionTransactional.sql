CREATE TABLE [dbo].[tblConcessionTransactional] (
    [pkConcessionTransactionalId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]              INT             NULL,
    [fkTransactionTypeId]         INT             NULL,
    [fkChannelTypeId]             INT             NULL,
    [TableNumber]                 INT             NULL,
    [TransactionVolume]           INT             NULL,
    [TransactionValue]            DECIMAL (18, 2) NULL,
    [fkBaseRateId]                INT             NULL,
    [AdValorem]                   DECIMAL (18)    NULL,
    CONSTRAINT [PK_tblConcessionTransactional] PRIMARY KEY CLUSTERED ([pkConcessionTransactionalId] ASC),
    CONSTRAINT [FK_tblConcessionTransactional_rtblBaseRate] FOREIGN KEY ([fkBaseRateId]) REFERENCES [dbo].[rtblBaseRate] ([pkBaseRateId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblConcessionTransactional_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

