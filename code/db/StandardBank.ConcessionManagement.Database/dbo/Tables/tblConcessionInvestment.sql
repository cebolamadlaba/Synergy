CREATE TABLE [dbo].[tblConcessionInvestment] (
    [pkConcessionInvestmentId] INT          IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]           INT          NOT NULL,
    [fkProductTypeId]          INT          NOT NULL,
    [Balance]                  DECIMAL (18) NOT NULL,
    [Term]                     INT          NOT NULL,
    [InterestToCustomer]       DECIMAL (18) NOT NULL,
    CONSTRAINT [PK_tblConcessionInvestment] PRIMARY KEY CLUSTERED ([pkConcessionInvestmentId] ASC),
    CONSTRAINT [FK_tblConcessionInvestment_rtblProduct] FOREIGN KEY ([fkProductTypeId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblConcessionInvestment_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

