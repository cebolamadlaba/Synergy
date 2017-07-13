CREATE TABLE [dbo].[tblBusinesOnlineTransactionType] (
    [pkBusinesOnlineTransactionTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [fkTransactionGroupId]             INT           NOT NULL,
    [Description]                      VARCHAR (100) NOT NULL,
    [IsActive]                         BIT           NOT NULL,
    CONSTRAINT [PK_tblTransactionTypeGroup] PRIMARY KEY CLUSTERED ([pkBusinesOnlineTransactionTypeId] ASC),
    CONSTRAINT [FK_tblTransactionTypeGroup_rtblTransactionGroup] FOREIGN KEY ([fkTransactionGroupId]) REFERENCES [dbo].[rtblTransactionGroup] ([pkTransactionGroupId])
);

