CREATE TABLE [dbo].[rtblTradeProduct] (
    [pkTradeProductId]     INT           IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (250) NULL,
    [fkTradeProductTypeId] INT           NULL,
    CONSTRAINT [PK_rtblTradeCode] PRIMARY KEY CLUSTERED ([pkTradeProductId] ASC),
    CONSTRAINT [FK_rtblTradeCode_rtblTradeCodeType] FOREIGN KEY ([fkTradeProductTypeId]) REFERENCES [dbo].[rtblTradeProductType] ([pkTradeProductTypeId])
);

