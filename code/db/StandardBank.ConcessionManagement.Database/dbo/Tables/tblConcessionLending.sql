CREATE TABLE [dbo].[tblConcessionLending] (
    [pkConcessionLendingId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]        INT             NOT NULL,
    [fkConcessionDetailId]  INT             NOT NULL,
    [fkProductTypeId]       INT             NOT NULL,
    [fkReviewFeeTypeId]     INT             NULL,
    [Limit]                 DECIMAL (18, 2) NULL,
    [Term]                  INT             NULL,
    [MarginToPrime]         DECIMAL (18, 2) NULL,
    [ApprovedMarginToPrime] DECIMAL (18, 2) NULL,
    [LoadedMarginToPrime]   DECIMAL (18, 2) NULL,
    [InitiationFee]         DECIMAL (18, 2) NULL,
    [ReviewFee]             DECIMAL (18, 2) NULL,
    [UFFFee]                DECIMAL (18, 2) NULL,
    [AverageBalance]        DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblConcessionLending] PRIMARY KEY CLUSTERED ([pkConcessionLendingId] ASC),
    CONSTRAINT [FK_tblConcessionLending_rtblProductType] FOREIGN KEY ([fkProductTypeId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblConcessionLending_rtblReviewFeeType] FOREIGN KEY ([fkReviewFeeTypeId]) REFERENCES [dbo].[rtblReviewFeeType] ([pkReviewFeeTypeId]),
    CONSTRAINT [FK_tblConcessionLending_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionLending_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);





