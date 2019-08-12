CREATE TABLE [dbo].[rtblBOLChargeCode] (
    [pkChargeCodeId]         INT            IDENTITY (1, 1) NOT NULL,
    [Description]            VARCHAR (250)  NULL,
    [ChargeCode]             VARCHAR (50)   NULL,
    [Length]                 INT            NULL,
    [fkChargeCodeTypeId]     INT            NULL,
    [IsActive]               BIT            NULL,
    [StandardPricingOption1] DECIMAL (7, 3) NULL,
    [StandardPricingOption2] DECIMAL (7, 3) NULL,
    [StandardPricingOption3] DECIMAL (7, 3) NULL,
    [IsNonUniversal]         BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_rtblChargeCode] PRIMARY KEY CLUSTERED ([pkChargeCodeId] ASC),
    CONSTRAINT [FK_rtblChargeCode_rtblChargeCodeType] FOREIGN KEY ([fkChargeCodeTypeId]) REFERENCES [dbo].[rtblBOLChargeCodeType] ([pkChargeCodeTypeId])
);



