CREATE TABLE [dbo].[tblRiskGroup] (
    [pkRiskGroupId]     INT           IDENTITY (0, 1) NOT NULL,
    [fkMarketSegmentId] INT           NOT NULL,
    [fkRegionId]        INT           NOT NULL,
    [RiskGroupNumber]   INT           NOT NULL,
    [RiskGroupName]     VARCHAR (200) NOT NULL,
    [IsActive]          BIT           NOT NULL,
    CONSTRAINT [PK_tblRiskGroup] PRIMARY KEY CLUSTERED ([pkRiskGroupId] ASC),
    CONSTRAINT [FK_tblRiskGroup_rtblMarketSegment] FOREIGN KEY ([fkMarketSegmentId]) REFERENCES [dbo].[rtblMarketSegment] ([pkMarketSegmentId]),
    CONSTRAINT [FK_tblRiskGroup_rtblRegion] FOREIGN KEY ([fkRegionId]) REFERENCES [dbo].[rtblRegion] ([pkRegionId])
);





