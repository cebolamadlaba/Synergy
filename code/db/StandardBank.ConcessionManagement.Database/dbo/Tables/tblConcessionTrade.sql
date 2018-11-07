CREATE TABLE [dbo].[tblConcessionTrade] (
    [pkConcessionTradeId]    INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]         INT             NOT NULL,
    [fkConcessionDetailId]   INT             NOT NULL,
    [fkTradeProductId]       INT             NULL,
    [fkLegalEntityAccountId] INT             NULL,
    [LoadedRate]             INT             NULL,
    [ApprovedRate]           INT             NULL,
    [GBBNumber]              VARCHAR (250)   NULL,
    [Term]                   INT             NULL,
    [Min]                    DECIMAL (18, 2) NULL,
    [Max]                    DECIMAL (18, 2) NULL,
    [Communication]          VARCHAR (250)   NULL,
    [FlatFee]                DECIMAL (18, 2) NULL,
    [EstablishmentFee]       DECIMAL (18, 2) NULL,
    [AdValorem]              DECIMAL (18, 2) NULL,
    [Currency]               VARCHAR (5)     NULL,
    [fkLegalEntityGBBNumber] INT             NULL,
    [fkLegalEntityId]        INT             NULL,
    CONSTRAINT [PK_tblConcessionTrade] PRIMARY KEY CLUSTERED ([pkConcessionTradeId] ASC),
    CONSTRAINT [FK_tblConcessionTrade_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionTrade_tblConcessionDetail] FOREIGN KEY ([fkConcessionDetailId]) REFERENCES [dbo].[tblConcessionDetail] ([pkConcessionDetailId])
);







