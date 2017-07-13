CREATE TABLE [dbo].[rtblConditionProduct] (
    [pkConditionProductId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (50) NOT NULL,
    [IsActive]             BIT          NOT NULL,
    CONSTRAINT [PK_rtblConditionProduct] PRIMARY KEY CLUSTERED ([pkConditionProductId] ASC)
);

