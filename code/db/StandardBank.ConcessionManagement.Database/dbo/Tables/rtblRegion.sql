CREATE TABLE [dbo].[rtblRegion] (
    [pkRegionId]  INT          IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [IsActive]    BIT          CONSTRAINT [DF_rtblRegion_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblRegion] PRIMARY KEY CLUSTERED ([pkRegionId] ASC)
);

