CREATE TABLE [dbo].[rtblConditionType] (
    [pkConditionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]       VARCHAR (50) NOT NULL,
    [IsActive]          BIT          NOT NULL,
    CONSTRAINT [PK_rtblConditionType] PRIMARY KEY CLUSTERED ([pkConditionTypeId] ASC)
);

