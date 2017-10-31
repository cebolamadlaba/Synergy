CREATE TABLE [dbo].[tblUserRegion] (
    [pkUserRegionId] INT IDENTITY (1, 1) NOT NULL,
    [fkUserId]       INT NOT NULL,
    [fkRegionId]     INT NOT NULL,
    [IsActive]       BIT CONSTRAINT [DF_tblUserRegion_IsActive] DEFAULT ((1)) NOT NULL,
    [IsSelected]     BIT CONSTRAINT [DF_tblUserRegion_IsSelected] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblUserRegion] PRIMARY KEY CLUSTERED ([pkUserRegionId] ASC),
    CONSTRAINT [FK_tblUserRegion_rtblRegion] FOREIGN KEY ([fkRegionId]) REFERENCES [dbo].[rtblRegion] ([pkRegionId]),
    CONSTRAINT [FK_tblUserRegion_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);



