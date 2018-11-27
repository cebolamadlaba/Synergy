CREATE TABLE [dbo].[rtblBOLChargeCode] (
    [pkChargeCodeId]     INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (250) NULL,
    [ChargeCode]         VARCHAR (50)  NULL,
    [Length]             INT           NULL,
    [fkChargeCodeTypeId] INT           NULL,
    [IsActive]           BIT           NULL,
    CONSTRAINT [PK_rtblChargeCode] PRIMARY KEY CLUSTERED ([pkChargeCodeId] ASC),
    CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType] FOREIGN KEY ([fkChargeCodeTypeId]) REFERENCES [dbo].[rtblBOLChargeCodeType] ([pkChargeCodeTypeId])
);

