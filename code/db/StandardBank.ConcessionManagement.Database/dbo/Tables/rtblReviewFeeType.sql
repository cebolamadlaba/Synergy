﻿CREATE TABLE [dbo].[rtblReviewFeeType] (
    [pkReviewFeeTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]       VARCHAR (50) NOT NULL,
    [IsActive]          BIT          NOT NULL,
    CONSTRAINT [PK_rtblReviewFeeType] PRIMARY KEY CLUSTERED ([pkReviewFeeTypeId] ASC)
);

