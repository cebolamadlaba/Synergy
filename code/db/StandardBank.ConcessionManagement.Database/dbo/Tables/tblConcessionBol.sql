CREATE TABLE [dbo].[tblConcessionBol] (
    [pkConcessionBolId]                INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]                   INT             NOT NULL,
    [fkConcessionDetailId]             INT             NOT NULL,
    [fkTransactionGroupId]             INT             NULL,
    [fkBusinesOnlineTransactionTypeId] INT             NULL,
    [BolUseId]                         INT             NULL,
    [TransactionVolume]                INT             NULL,
    [TransactionValue]                 DECIMAL (18, 2) NULL,
    [Fee]                              DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionBol] PRIMARY KEY CLUSTERED ([pkConcessionBolId] ASC),
    CONSTRAINT [FK_tblConcessionBol_rtblTransactionGroup] FOREIGN KEY ([fkTransactionGroupId]) REFERENCES [dbo].[rtblTransactionGroup] ([pkTransactionGroupId]),
    CONSTRAINT [FK_tblConcessionBol_tblBolUser] FOREIGN KEY ([BolUseId]) REFERENCES [dbo].[tblBolUser] ([pkBolUserId]),
    CONSTRAINT [FK_tblConcessionBol_tblBusinesOnlineTransactionType] FOREIGN KEY ([fkBusinesOnlineTransactionTypeId]) REFERENCES [dbo].[tblBusinesOnlineTransactionType] ([pkBusinesOnlineTransactionTypeId]),
    CONSTRAINT [FK_tblConcessionBol_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionBol_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);





