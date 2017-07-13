CREATE TABLE [dbo].[tblChannelTypeBaseRate] (
    [pkChannelTypeBaseRateId] INT IDENTITY (1, 1) NOT NULL,
    [fkChannelTypeId]         INT NOT NULL,
    [fkBaseRateId]            INT NOT NULL,
    CONSTRAINT [PK_tblChannelTypeBaseRate] PRIMARY KEY CLUSTERED ([pkChannelTypeBaseRateId] ASC),
    CONSTRAINT [FK_tblChannelTypeBaseRate_rtblBaseRate] FOREIGN KEY ([fkBaseRateId]) REFERENCES [dbo].[rtblBaseRate] ([pkBaseRateId]),
    CONSTRAINT [FK_tblChannelTypeBaseRate_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId])
);

