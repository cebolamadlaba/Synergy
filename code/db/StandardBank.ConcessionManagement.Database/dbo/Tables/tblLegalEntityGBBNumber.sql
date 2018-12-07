CREATE TABLE [dbo].[tblLegalEntityGBBNumber] (
    [pkLegalEntityGBBNumber] INT           IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityAccountId] INT           NULL,
    [GBBNumber]              VARCHAR (250) NULL,
    CONSTRAINT [PK_LegalEntityGBBNumber] PRIMARY KEY CLUSTERED ([pkLegalEntityGBBNumber] ASC),
    CONSTRAINT [FK_LegalEntityGBBNumber_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

