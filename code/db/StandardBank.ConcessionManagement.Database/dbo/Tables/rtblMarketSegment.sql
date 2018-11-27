CREATE TABLE [dbo].[rtblMarketSegment] (
    [pkMarketSegmentId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]       VARCHAR (100) NOT NULL,
    [IsActive]          BIT           NOT NULL,
    [BCMRoleName]       VARCHAR (250) NULL,
    [RequestorRoleName] VARCHAR (250) NULL,
    CONSTRAINT [PK_rtblMarketSegment] PRIMARY KEY CLUSTERED ([pkMarketSegmentId] ASC)
);



