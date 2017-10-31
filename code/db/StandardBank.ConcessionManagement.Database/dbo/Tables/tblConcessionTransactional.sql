CREATE TABLE [dbo].[tblConcessionTransactional] (
    [pkConcessionTransactionalId]        INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]                     INT             NOT NULL,
    [fkConcessionDetailId]               INT             NOT NULL,
    [fkTransactionTypeId]                INT             NOT NULL,
    [fkTransactionTableNumberId]         INT             NOT NULL,
    [fkApprovedTransactionTableNumberId] INT             NULL,
    [fkLoadedTransactionTableNumberId]   INT             NULL,
    [Fee]                                DECIMAL (18, 3) NULL,
    [AdValorem]                          DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionTransactional] PRIMARY KEY CLUSTERED ([pkConcessionTransactionalId] ASC),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTransactionTableNumber] FOREIGN KEY ([fkTransactionTableNumberId]) REFERENCES [dbo].[rtblTransactionTableNumber] ([pkTransactionTableNumberId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTransactionTableNumber_approved] FOREIGN KEY ([fkApprovedTransactionTableNumberId]) REFERENCES [dbo].[rtblTransactionTableNumber] ([pkTransactionTableNumberId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTransactionTableNumber_loaded] FOREIGN KEY ([fkLoadedTransactionTableNumberId]) REFERENCES [dbo].[rtblTransactionTableNumber] ([pkTransactionTableNumberId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblConcessionTransactional_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionTransactional_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);







