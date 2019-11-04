CREATE TABLE [dbo].[tblBaseRateCode] (
    [pkBaseRateCodeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]      VARCHAR (50) NOT NULL,
    [IsActive]         BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([pkBaseRateCodeId] ASC)
);

