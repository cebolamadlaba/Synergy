CREATE TABLE [dbo].[rtblStatus] (
    [pkStatusId]  INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [IsActive]    BIT          NOT NULL,
    CONSTRAINT [PK_rtblStatus] PRIMARY KEY CLUSTERED ([pkStatusId] ASC)
);

