CREATE TABLE [dbo].[rtblTradeProductType] (
    [pkTradeProductTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (250) NULL,
    CONSTRAINT [PK_rtblTradeCodeType] PRIMARY KEY CLUSTERED ([pkTradeProductTypeId] ASC)
);

