CREATE TABLE [dbo].[rtblTableNumber] (
    [pkTableNumberId]    INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionTypeId] INT             NOT NULL,
    [TariffTable]        INT             NOT NULL,
    [AdValorem]          DECIMAL (18, 2) NULL,
    [BaseRate]           DECIMAL (18, 3) NULL,
    [IsActive]           BIT             CONSTRAINT [DF_rtblTableNumber_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblTableNumber] PRIMARY KEY CLUSTERED ([pkTableNumberId] ASC),
    CONSTRAINT [FK_rtblTableNumber_rtblConcessionType] FOREIGN KEY ([fkConcessionTypeId]) REFERENCES [dbo].[rtblConcessionType] ([pkConcessionTypeId])
);

