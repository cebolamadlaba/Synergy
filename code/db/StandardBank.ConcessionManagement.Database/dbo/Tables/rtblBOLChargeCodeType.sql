CREATE TABLE [dbo].[rtblBOLChargeCodeType] (
    [pkChargeCodeTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (250) NULL,
    CONSTRAINT [PK_rtblChargeCodeType] PRIMARY KEY CLUSTERED ([pkChargeCodeTypeId] ASC)
);

