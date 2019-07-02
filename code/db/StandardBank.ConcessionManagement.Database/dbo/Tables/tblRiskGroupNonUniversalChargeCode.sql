CREATE TABLE [dbo].[tblRiskGroupNonUniversalChargeCode] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [RiskGroupId]  INT NOT NULL,
    [ChargeCodeId] INT NULL,
    [IsActive]     BIT DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

