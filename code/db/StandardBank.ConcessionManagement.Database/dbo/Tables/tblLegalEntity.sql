CREATE TABLE [dbo].[tblLegalEntity] (
    [pkLegalEntityId]   INT           IDENTITY (1, 1) NOT NULL,
    [fkMarketSegmentId] INT           NOT NULL,
    [fkRiskGroupId]     INT           NOT NULL,
    [fkUserId]          INT           NULL,
    [CustomerName]      VARCHAR (200) NOT NULL,
    [CustomerNumber]    VARCHAR (20)  NULL,
    [IsActive]          BIT           NOT NULL,
    [ContactPerson]     VARCHAR (150) NULL,
    [PostalAddress]     VARCHAR (250) NULL,
    [City]              VARCHAR (150) NULL,
    [PostalCode]        VARCHAR (50)  NULL,
    [AE_A-Number]       NCHAR (10)    NULL,
    CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED ([pkLegalEntityId] ASC),
    CONSTRAINT [FK_tblCustomer_rtblMarketSegment] FOREIGN KEY ([fkMarketSegmentId]) REFERENCES [dbo].[rtblMarketSegment] ([pkMarketSegmentId]),
    CONSTRAINT [FK_tblCustomer_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId]),
    CONSTRAINT [FK_tblLegalEntity_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);











