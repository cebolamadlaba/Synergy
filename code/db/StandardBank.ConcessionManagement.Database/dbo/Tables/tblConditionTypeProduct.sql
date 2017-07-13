CREATE TABLE [dbo].[tblConditionTypeProduct] (
    [pkConditionTypeProductId] INT IDENTITY (1, 1) NOT NULL,
    [fkConditionTypeId]        INT NOT NULL,
    [fkConditionProductId]     INT NOT NULL,
    [IsActive]                 BIT NOT NULL,
    CONSTRAINT [PK_tblConditionTypeProduct] PRIMARY KEY CLUSTERED ([pkConditionTypeProductId] ASC),
    CONSTRAINT [FK_tblConditionTypeProduct_rtblConditionProduct] FOREIGN KEY ([fkConditionProductId]) REFERENCES [dbo].[rtblConditionProduct] ([pkConditionProductId]),
    CONSTRAINT [FK_tblConditionTypeProduct_rtblConditionType] FOREIGN KEY ([fkConditionTypeId]) REFERENCES [dbo].[rtblConditionType] ([pkConditionTypeId])
);

