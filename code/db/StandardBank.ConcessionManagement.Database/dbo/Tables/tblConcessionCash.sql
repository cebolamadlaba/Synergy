CREATE TABLE [dbo].[tblConcessionCash] (
    [pkConcessionCashId]      INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]          INT             NOT NULL,
    [fkConcessionDetailId]    INT             NOT NULL,
    [fkChannelTypeId]         INT             NOT NULL,
    [fkAccrualTypeId]         INT             NOT NULL,
    [fkTableNumberId]         INT             NOT NULL,
    [fkApprovedTableNumberId] INT             NULL,
    [fkLoadedTableNumberId]   INT             NULL,
    [AdValorem]               DECIMAL (18, 3) NULL,
    [BaseRate]                DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionCash] PRIMARY KEY CLUSTERED ([pkConcessionCashId] ASC),
    CONSTRAINT [FK_tblConcessionCash_rtblAccrualType] FOREIGN KEY ([fkAccrualTypeId]) REFERENCES [dbo].[rtblAccrualType] ([pkAccrualTypeId]),
    CONSTRAINT [FK_tblConcessionCash_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId]),
    CONSTRAINT [FK_tblConcessionCash_rtblTableNumber] FOREIGN KEY ([fkTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblConcessionCash_rtblTableNumber_approved] FOREIGN KEY ([fkApprovedTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblConcessionCash_rtblTableNumber_loaded] FOREIGN KEY ([fkLoadedTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblConcessionCash_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionCash_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);







