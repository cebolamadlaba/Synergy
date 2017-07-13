CREATE TABLE [dbo].[rtblAdValorem] (
    [pkAdValoremId] INT             IDENTITY (1, 1) NOT NULL,
    [AdValorem]     DECIMAL (18, 2) NULL,
    [IsActive]      BIT             NOT NULL,
    CONSTRAINT [PK_rtblAdValorem] PRIMARY KEY CLUSTERED ([pkAdValoremId] ASC)
);

