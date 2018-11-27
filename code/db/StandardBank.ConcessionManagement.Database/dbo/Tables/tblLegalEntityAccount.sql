CREATE TABLE [dbo].[tblLegalEntityAccount] (
    [pkLegalEntityAccountId] INT          IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityId]        INT          NOT NULL,
    [AccountNumber]          VARCHAR (20) NOT NULL,
    [IsActive]               BIT          NOT NULL,
    CONSTRAINT [PK_tblLegalEntityAccount] PRIMARY KEY CLUSTERED ([pkLegalEntityAccountId] ASC),
    CONSTRAINT [FK_tblLegalEntityAccount_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
);








GO


