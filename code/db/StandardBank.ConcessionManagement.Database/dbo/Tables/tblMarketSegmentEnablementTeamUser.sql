CREATE TABLE [dbo].[tblMarketSegmentEnablementTeamUser] (
    [pkMarketSegmentEnablementTeamId] INT IDENTITY (1, 1) NOT NULL,
    [fkMarketSegmentId]               INT NOT NULL,
    [fkUserId]                        INT NOT NULL,
    [IsActive]                        BIT DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([pkMarketSegmentEnablementTeamId] ASC),
    FOREIGN KEY ([fkMarketSegmentId]) REFERENCES [dbo].[rtblMarketSegment] ([pkMarketSegmentId]),
    FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [UC_MarketSegmentEnablementTeamUser] UNIQUE NONCLUSTERED ([fkMarketSegmentId] ASC, [fkUserId] ASC)
);

