CREATE TABLE [dbo].[tblConcessionGlms] (
    [pkConcessionGlmsId]          INT IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]              INT NOT NULL,
    [fkConcessionDetailId]        INT NOT NULL,
    [fkProductId]                 INT NOT NULL,
    [fkLegalEntityAccountId]      INT NOT NULL,
    [fkGroupId]                   INT NULL,
    [fkInterestPricingCategoryId] INT NULL,
    [fkSlabTypeId]                INT NULL,
    [fkInterestTypeId]            INT NULL,
    PRIMARY KEY CLUSTERED ([pkConcessionGlmsId] ASC),
    FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId]),
    FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    FOREIGN KEY ([fkInterestPricingCategoryId]) REFERENCES [dbo].[tblInterestPricingCategory] ([pkInterestPricingCategoryId]),
    FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

