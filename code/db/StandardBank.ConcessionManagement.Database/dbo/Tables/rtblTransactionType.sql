CREATE TABLE [dbo].[rtblTransactionType] (
    [pkTransactionTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [fkConcessionTypeId]  INT           NULL,
    [Description]         VARCHAR (250) NOT NULL,
    [IsActive]            BIT           NOT NULL,
    CONSTRAINT [PK_rtblTransactionType] PRIMARY KEY CLUSTERED ([pkTransactionTypeId] ASC),
    CONSTRAINT [FK_rtblTransactionType_rtblConcessionType] FOREIGN KEY ([fkConcessionTypeId]) REFERENCES [dbo].[rtblConcessionType] ([pkConcessionTypeId])
);









