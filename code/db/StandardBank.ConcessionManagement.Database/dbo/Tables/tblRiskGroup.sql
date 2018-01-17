﻿CREATE TABLE [dbo].[tblRiskGroup] (
    [pkRiskGroupId]     INT           IDENTITY (0, 1) NOT NULL,
    [fkMarketSegmentId] INT           NOT NULL,
    [RiskGroupNumber]   INT           NOT NULL,
    [RiskGroupName]     VARCHAR (200) NOT NULL,
    [IsActive]          BIT           NOT NULL,
    CONSTRAINT [PK_tblRiskGroup] PRIMARY KEY CLUSTERED ([pkRiskGroupId] ASC),
    CONSTRAINT [FK_tblRiskGroup_rtblMarketSegment] FOREIGN KEY ([fkMarketSegmentId]) REFERENCES [dbo].[rtblMarketSegment] ([pkMarketSegmentId])
);










GO
CREATE NONCLUSTERED INDEX [IX_tblRiskGroup]
    ON [dbo].[tblRiskGroup]([RiskGroupNumber] ASC);

