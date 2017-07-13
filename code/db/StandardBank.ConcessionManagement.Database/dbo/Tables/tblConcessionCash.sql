CREATE TABLE [dbo].[tblConcessionCash] (
    [pkConcessionCashId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]     INT             NOT NULL,
    [fkChannelTypeId]    INT             NOT NULL,
    [TableNumber]        INT             NULL,
    [CashVolume]         INT             NULL,
    [CashValue]          DECIMAL (18, 2) NULL,
    [fkBaseRateId]       INT             NULL,
    [AdValorem]          DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionCash] PRIMARY KEY CLUSTERED ([pkConcessionCashId] ASC),
    CONSTRAINT [FK_tblConcessionCash_rtblBaseRate] FOREIGN KEY ([fkBaseRateId]) REFERENCES [dbo].[rtblBaseRate] ([pkBaseRateId]),
    CONSTRAINT [FK_tblConcessionCash_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId]),
    CONSTRAINT [FK_tblConcessionCash_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

