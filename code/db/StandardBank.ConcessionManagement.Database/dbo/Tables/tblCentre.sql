CREATE TABLE [dbo].[tblCentre] (
    [pkCentreId]   INT           IDENTITY (1, 1) NOT NULL,
    [fkProvinceId] INT           NOT NULL,
    [CentreName]   VARCHAR (100) NOT NULL,
    [IsActive]     BIT           CONSTRAINT [DF_tblCentre_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblCentre] PRIMARY KEY CLUSTERED ([pkCentreId] ASC),
    CONSTRAINT [FK_tblCentre_rtblProvince] FOREIGN KEY ([fkProvinceId]) REFERENCES [dbo].[rtblProvince] ([pkProvinceId])
);

