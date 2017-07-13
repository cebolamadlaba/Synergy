CREATE TABLE [dbo].[tblScenarioManagerToolDeal] (
    [pkScenarioManagerToolDealId] INT          IDENTITY (1, 1) NOT NULL,
    [DealNumber]                  VARCHAR (20) NOT NULL,
    [IsActive]                    BIT          NOT NULL,
    CONSTRAINT [PK_tblScenarioManagerToolDeal] PRIMARY KEY CLUSTERED ([pkScenarioManagerToolDealId] ASC)
);

