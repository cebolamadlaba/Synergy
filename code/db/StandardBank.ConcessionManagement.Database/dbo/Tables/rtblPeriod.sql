CREATE TABLE [dbo].[rtblPeriod] (
    [pkPeriodId]  INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [IsActive]    BIT          CONSTRAINT [DF_rtblPeriod_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblPeriod] PRIMARY KEY CLUSTERED ([pkPeriodId] ASC)
);

