CREATE TABLE [dbo].[rtblProduct] (
    [pkProductId]        INT           IDENTITY (1, 1) NOT NULL,
    [fkConcessionTypeId] INT           NOT NULL,
    [Description]        VARCHAR (100) NOT NULL,
    [IsActive]           BIT           NOT NULL,
    CONSTRAINT [PK_rtblProductType] PRIMARY KEY CLUSTERED ([pkProductId] ASC)
);

