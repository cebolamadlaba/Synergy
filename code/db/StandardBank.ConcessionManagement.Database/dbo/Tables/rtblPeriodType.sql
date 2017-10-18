CREATE TABLE [dbo].[rtblPeriodType] (
    [pkPeriodTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]    VARCHAR (50) NOT NULL,
    [IsActive]       BIT          CONSTRAINT [DF_rtblPeriodType_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblPeriodType] PRIMARY KEY CLUSTERED ([pkPeriodTypeId] ASC)
);

