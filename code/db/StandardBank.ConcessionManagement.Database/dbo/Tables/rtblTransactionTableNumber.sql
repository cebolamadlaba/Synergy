CREATE TABLE [dbo].[rtblTransactionTableNumber] (
    [pkTransactionTableNumberId] INT             IDENTITY (1, 1) NOT NULL,
    [fkTransactionTypeId]        INT             NOT NULL,
    [TariffTable]                INT             NOT NULL,
    [Fee]                        DECIMAL (18, 2) NULL,
    [AdValorem]                  DECIMAL (18, 3) NULL,
    [IsActive]                   BIT             CONSTRAINT [DF_rtblTransactionTableNumber_IsActive] DEFAULT ((1)) NOT NULL,
    [ActiveUntil]                DATETIME        NULL,
    CONSTRAINT [PK_rtblTransactionTableNumber] PRIMARY KEY CLUSTERED ([pkTransactionTableNumberId] ASC),
    CONSTRAINT [FK_rtblTransactionTableNumber_rtblTransactionType] FOREIGN KEY ([fkTransactionTypeId]) REFERENCES [dbo].[rtblTransactionType] ([pkTransactionTypeId])
);



