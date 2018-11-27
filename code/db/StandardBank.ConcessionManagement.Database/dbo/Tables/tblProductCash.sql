CREATE TABLE [dbo].[tblProductCash] (
    [pkProductCashId]        INT             IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT             NOT NULL,
    [fkLegalEntityId]        INT             NOT NULL,
    [fkLegalEntityAccountId] INT             NOT NULL,
    [fkTableNumberId]        INT             NOT NULL,
    [Channel]                VARCHAR (150)   NOT NULL,
    [BpId]                   INT             NOT NULL,
    [Volume]                 DECIMAL (18, 2) NOT NULL,
    [Value]                  DECIMAL (18, 2) NOT NULL,
    [LoadedPrice]            VARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_tblProductCash] PRIMARY KEY CLUSTERED ([pkProductCashId] ASC),
    CONSTRAINT [FK_tblProductCash_rtblTableNumber] FOREIGN KEY ([fkTableNumberId]) REFERENCES [dbo].[rtblTableNumber] ([pkTableNumberId]),
    CONSTRAINT [FK_tblProductCash_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductCash_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId]),
    CONSTRAINT [FK_tblProductCash_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);



