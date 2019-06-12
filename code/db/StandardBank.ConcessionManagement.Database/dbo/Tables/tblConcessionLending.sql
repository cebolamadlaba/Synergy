CREATE TABLE [dbo].[tblConcessionLending] (
    [pkConcessionLendingId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]        INT             NOT NULL,
    [fkConcessionDetailId]  INT             NOT NULL,
    [fkProductTypeId]       INT             NOT NULL,
    [fkReviewFeeTypeId]     INT             NULL,
    [Limit]                 DECIMAL (18, 2) NULL,
    [Term]                  INT             NULL,
    [MarginToPrime]         DECIMAL (18, 3) NULL,
    [ApprovedMarginToPrime] DECIMAL (18, 3) NULL,
    [LoadedMarginToPrime]   DECIMAL (18, 3) NULL,
    [InitiationFee]         DECIMAL (18, 3) NULL,
    [ReviewFee]             DECIMAL (18, 3) NULL,
    [UFFFee]                DECIMAL (18, 3) NULL,
    [AverageBalance]        DECIMAL (18, 2) NULL,
    [Frequency]             VARCHAR (50)    NULL,
    [ServiceFee]            DECIMAL (18, 3) NULL,
    [MRS_BRI] INT NOT NULL, 
    CONSTRAINT [PK_tblConcessionLending] PRIMARY KEY CLUSTERED ([pkConcessionLendingId] ASC),
    CONSTRAINT [FK_tblConcessionLending_rtblProductType] FOREIGN KEY ([fkProductTypeId]) REFERENCES [dbo].[rtblProduct] ([pkProductId]),
    CONSTRAINT [FK_tblConcessionLending_rtblReviewFeeType] FOREIGN KEY ([fkReviewFeeTypeId]) REFERENCES [dbo].[rtblReviewFeeType] ([pkReviewFeeTypeId]),
    CONSTRAINT [FK_tblConcessionLending_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionLending_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);









