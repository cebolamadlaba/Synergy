CREATE TABLE [dbo].[rtblConcessionType] (
    [pkConcessionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (50) NOT NULL,
    [Code]               VARCHAR (15) NULL,
    [IsActive]           BIT          NOT NULL,
    CONSTRAINT [PK_rtblConcessionType] PRIMARY KEY CLUSTERED ([pkConcessionTypeId] ASC)
);

