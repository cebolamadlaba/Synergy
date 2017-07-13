CREATE TABLE [dbo].[rtblProvince] (
    [pkProvinceId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (100) NOT NULL,
    [IsActive]     BIT           NOT NULL,
    CONSTRAINT [PK_rtblProvince] PRIMARY KEY CLUSTERED ([pkProvinceId] ASC)
);

