CREATE TABLE [dbo].[tblFinancialLending] (
    [pkFinancialLendingId] INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]        INT             NOT NULL,
    [TotalExposure]        DECIMAL (18, 2) NOT NULL,
    [WeightedAverageMap]   DECIMAL (18, 2) NOT NULL,
    [WeightedCrsOrMrs]     DECIMAL (18, 2) NOT NULL,
    [LatestCrsOrMrs]       DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblFinancialLending] PRIMARY KEY CLUSTERED ([pkFinancialLendingId] ASC),
    CONSTRAINT [FK_tblFinancialLending_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);

