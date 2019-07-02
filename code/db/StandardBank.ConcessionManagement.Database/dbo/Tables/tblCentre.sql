CREATE TABLE [dbo].[tblCentre] (
    [pkCentreId] INT           IDENTITY (1, 1) NOT NULL,
    [fkRegionId] INT           NOT NULL,
    [CentreName] VARCHAR (100) NOT NULL,
    [IsActive]   BIT           CONSTRAINT [DF_tblCentre_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblCentre] PRIMARY KEY CLUSTERED ([pkCentreId] ASC),
    CONSTRAINT [FK_tblCentre_rtblRegion] FOREIGN KEY ([fkRegionId]) REFERENCES [dbo].[rtblRegion] ([pkRegionId])
);







