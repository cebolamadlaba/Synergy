CREATE TABLE [dbo].[rtblBaseRate] (
    [pkBaseRateId] INT             IDENTITY (1, 1) NOT NULL,
    [BaseRate]     DECIMAL (18, 2) NOT NULL,
    [IsActive]     BIT             NOT NULL,
    CONSTRAINT [PK_rtblBaseRate] PRIMARY KEY CLUSTERED ([pkBaseRateId] ASC)
);

