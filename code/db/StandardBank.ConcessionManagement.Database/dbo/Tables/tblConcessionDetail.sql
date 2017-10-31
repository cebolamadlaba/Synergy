CREATE TABLE [dbo].[tblConcessionDetail] (
    [pkConcessionDetailId]   INT      IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]         INT      NOT NULL,
    [fkLegalEntityId]        INT      NOT NULL,
    [fkLegalEntityAccountId] INT      NOT NULL,
    [ExpiryDate]             DATETIME NULL,
    [DateApproved]           DATETIME NULL,
    [IsMismatched]           BIT      CONSTRAINT [DF_tblConcessionDetail_IsMismatched] DEFAULT ((0)) NOT NULL,
    [PriceExported]          BIT      DEFAULT ((0)) NOT NULL,
    [PriceExportedDate]      DATETIME NULL,
    CONSTRAINT [PK_tblConcessionDetail] PRIMARY KEY CLUSTERED ([pkConcessionDetailId] ASC),
    CONSTRAINT [FK_tblConcessionDetail_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionDetail_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblConcessionDetail_tblLegalEntityAccount] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

