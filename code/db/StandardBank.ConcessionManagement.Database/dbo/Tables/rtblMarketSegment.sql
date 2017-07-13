CREATE TABLE [dbo].[rtblMarketSegment] (
    [pkMarketSegmentId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]       VARCHAR (100) NOT NULL,
    [IsActive]          BIT           NOT NULL,
    CONSTRAINT [PK_rtblMarketSegment] PRIMARY KEY CLUSTERED ([pkMarketSegmentId] ASC)
);

