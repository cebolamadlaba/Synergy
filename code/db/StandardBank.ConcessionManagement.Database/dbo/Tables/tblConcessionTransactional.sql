CREATE TABLE [dbo].[tblConcessionTransactional] (
    [pkConcessionTransactionalId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]              INT             NOT NULL,
    [fkConcessionDetailId]        INT             NOT NULL,
    [fkTransactionTypeId]         INT             NULL,
    [fkTableNumberId]             INT             NOT NULL,
    [fkApprovedTableNumberId]     INT             NULL,
    [fkLoadedTableNumberId]       INT             NULL,
    [AdValorem]                   DECIMAL (18, 2) NULL,
    [BaseRate]                    DECIMAL (18, 3) NULL,
    CONSTRAINT [PK_tblConcessionTransactional] PRIMARY KEY CLUSTERED ([pkConcessionTransactionalId] ASC),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTableNumber] FOREIGN KEY ([fkTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTableNumber_approved] FOREIGN KEY ([fkApprovedTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTableNumber_loaded] FOREIGN KEY ([fkLoadedTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblConcessionTransactional_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblConcessionTransactional_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionTransactional_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);



