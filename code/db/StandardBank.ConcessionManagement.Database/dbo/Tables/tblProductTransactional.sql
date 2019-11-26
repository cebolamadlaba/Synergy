CREATE TABLE [dbo].[tblProductTransactional] (
    [pkProductTransactionalId]   INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]              INT             NULL,
    [fkLegalEntityId]            INT             NULL,
    [fkLegalEntityAccountId]     INT             NOT NULL,
    [fkTransactionTypeId]        INT             NOT NULL,
    [fkTransactionTableNumberId] INT             NOT NULL,
    [Volume]                     DECIMAL (18, 2) NULL,
    [Value]                      DECIMAL (18, 2) NULL,
    [LoadedPrice]                VARCHAR (50)    NULL,
    CONSTRAINT [PK_tblProductTransactional] PRIMARY KEY CLUSTERED ([pkProductTransactionalId] ASC),
    CONSTRAINT [FK_tblProductTransactional_rtblTransactionTableNumber] FOREIGN KEY ([fkTransactionTableNumberId]) REFERENCES [dbo].[rtblTransactionTableNumber] ([pkTransactionTableNumberId]),
    CONSTRAINT [FK_tblProductTransactional_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblProductTransactional_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductTransactional_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId]),
    CONSTRAINT [FK_tblProductTransactional_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);



