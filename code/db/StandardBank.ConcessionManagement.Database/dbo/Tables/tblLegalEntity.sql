﻿CREATE TABLE [dbo].[tblLegalEntity] (
    [pkLegalEntityId]   INT           IDENTITY (1, 1) NOT NULL,
    [fkMarketSegmentId] INT           NOT NULL,
    [fkRiskGroupId]     INT           NOT NULL,
    [RiskGroupName]     VARCHAR (200) NOT NULL,
    [CustomerName]      VARCHAR (200) NOT NULL,
    [CustomerNumber]    VARCHAR (20)  NULL,
    [IsActive]          BIT           NOT NULL,
    CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED ([pkLegalEntityId] ASC),
    CONSTRAINT [FK_tblCustomer_rtblMarketSegment] FOREIGN KEY ([fkMarketSegmentId]) REFERENCES [dbo].[rtblMarketSegment] ([pkMarketSegmentId]),
    CONSTRAINT [FK_tblCustomer_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);
