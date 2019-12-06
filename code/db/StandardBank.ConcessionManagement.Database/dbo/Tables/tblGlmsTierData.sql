CREATE TABLE [dbo].[tblGlmsTierData] (
    [GlmsTierDataId]     INT             IDENTITY (1, 1) NOT NULL,
    [fkGlmsConcessionId] INT             NOT NULL,
    [TierFrom]           DECIMAL (18, 2) NOT NULL,
    [TierTo]             DECIMAL (18, 2) NOT NULL,
    [fkRateTypeId]       INT             NULL,
    [fkBaseRateId]       INT             NULL,
    [Spread]             DECIMAL (18, 2) NULL,
    [Value]              DECIMAL (18, 2) NULL,
    PRIMARY KEY CLUSTERED ([GlmsTierDataId] ASC),
    FOREIGN KEY ([fkBaseRateId]) REFERENCES [dbo].[tblBaseRateCode] ([pkBaseRateCodeId]),
    FOREIGN KEY ([fkGlmsConcessionId]) REFERENCES [dbo].[tblConcessionGlms] ([pkConcessionGlmsId]),
    FOREIGN KEY ([fkRateTypeId]) REFERENCES [dbo].[tblRateType] ([pkRateTypeId])
);

