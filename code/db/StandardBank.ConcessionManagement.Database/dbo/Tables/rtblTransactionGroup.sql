CREATE TABLE [dbo].[rtblTransactionGroup] (
    [pkTransactionGroupId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]          VARCHAR (250) NOT NULL,
    [IsActive]             BIT           NOT NULL,
    CONSTRAINT [PK_rtblTransactionGroup] PRIMARY KEY CLUSTERED ([pkTransactionGroupId] ASC)
);

