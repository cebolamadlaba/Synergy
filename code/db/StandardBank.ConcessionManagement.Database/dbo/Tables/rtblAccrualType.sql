CREATE TABLE [dbo].[rtblAccrualType] (
    [pkAccrualTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]     VARCHAR (50) NOT NULL,
    [IsActive]        BIT          CONSTRAINT [DF_rtblAccrualType_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblAccrualType] PRIMARY KEY CLUSTERED ([pkAccrualTypeId] ASC)
);

