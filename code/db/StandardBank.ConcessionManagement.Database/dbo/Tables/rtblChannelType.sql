CREATE TABLE [dbo].[rtblChannelType] (
    [pkChannelTypeId]      INT          IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (50) NOT NULL,
    [IsActive]             BIT          NOT NULL,
    [StandardPricingTable] INT          NULL,
    CONSTRAINT [PK_rtblChannelType] PRIMARY KEY CLUSTERED ([pkChannelTypeId] ASC)
);









