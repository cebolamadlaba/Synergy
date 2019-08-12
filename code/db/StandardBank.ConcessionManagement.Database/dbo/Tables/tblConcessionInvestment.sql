CREATE TABLE [dbo].[tblConcessionInvestment] (
    [pkConcessionInvestmentId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]           INT             NOT NULL,
    [fkConcessionDetailId]     INT             NOT NULL,
    [fkProductId]              INT             NOT NULL,
    [fkLegalEntityAccountId]   INT             NOT NULL,
    [Balance]                  DECIMAL (18, 2) NULL,
    [Term]                     INT             NULL,
    [LoadedRate]               DECIMAL (18, 3) NULL,
    [ApprovedRate]             DECIMAL (18, 3) NULL,
    CONSTRAINT [PK_tblConcessionInvestment] PRIMARY KEY CLUSTERED ([pkConcessionInvestmentId] ASC),
    CONSTRAINT [FK_tblConcessionInvestment_rtblProduct] FOREIGN KEY ([fkProductId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblConcessionInvestment_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionInvestment_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);









