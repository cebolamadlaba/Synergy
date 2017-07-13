CREATE TABLE [dbo].[tblConcessionMas] (
    [pkConcessionMasId]   INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]      INT             NOT NULL,
    [fkTransactionTypeId] INT             NOT NULL,
    [MerchantNumber]      VARCHAR (25)    NOT NULL,
    [Turnover]            DECIMAL (18, 2) NULL,
    [CommissionRate]      DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionMas] PRIMARY KEY CLUSTERED ([pkConcessionMasId] ASC),
    CONSTRAINT [FK_tblConcessionMas_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId]),
    CONSTRAINT [FK_tblConcessionMas_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

