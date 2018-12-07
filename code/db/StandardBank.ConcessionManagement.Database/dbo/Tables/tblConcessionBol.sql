CREATE TABLE [dbo].[tblConcessionBol] (
    [pkConcessionBolId]      INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]         INT             NOT NULL,
    [fkConcessionDetailId]   INT             NOT NULL,
    [fkLegalEntityBOLUserId] INT             NULL,
    [fkChargeCodeId]         INT             NULL,
    [LoadedRate]             DECIMAL (18, 5) NULL,
    [ApprovedRate]           DECIMAL (18, 5) NULL,
    CONSTRAINT [PK_tblConcessionBol] PRIMARY KEY CLUSTERED ([pkConcessionBolId] ASC),
    CONSTRAINT [FK_tblConcessionBol_rtblBOLChargeCode] FOREIGN KEY ([fkChargeCodeId]) REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId]),
    CONSTRAINT [FK_tblConcessionBol_tblBolUser] FOREIGN KEY ([fkLegalEntityBOLUserId]) REFERENCES [dbo].[tblLegalEntityBOLUser] ([pkLegalEntityBOLUserId]),
    CONSTRAINT [FK_tblConcessionBol_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionBol_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);







