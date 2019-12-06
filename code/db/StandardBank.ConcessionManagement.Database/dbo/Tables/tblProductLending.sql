CREATE TABLE [dbo].[tblProductLending] (
    [pkProductLendingId]     INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT             NULL,
    [fkLegalEntityId]        INT             NOT NULL,
    [fkLegalEntityAccountId] INT             NOT NULL,
    [fkProductId]            INT             NOT NULL,
    [Limit]                  DECIMAL (18, 2) NOT NULL,
    [AverageBalance]         DECIMAL (18, 2) NOT NULL,
    [LoadedMap]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_tblProductLending] PRIMARY KEY CLUSTERED ([pkProductLendingId] ASC),
    CONSTRAINT [FK_tblProductLending_rtblProduct] FOREIGN KEY ([fkProductId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblProductLending_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductLending_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId]),
    CONSTRAINT [FK_tblProductLending_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);



