CREATE TABLE [dbo].[rtblType] (
    [pkTypeId]    INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [IsActive]    BIT          NOT NULL,
    CONSTRAINT [PK_rtblType] PRIMARY KEY CLUSTERED ([pkTypeId] ASC)
);

