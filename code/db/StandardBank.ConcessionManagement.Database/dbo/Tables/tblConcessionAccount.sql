CREATE TABLE [dbo].[tblConcessionAccount] (
    [pkConcessionAccountId] INT          IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]        INT          NOT NULL,
    [AccountNumber]         VARCHAR (20) NULL,
    [IsActive]              BIT          NOT NULL,
    CONSTRAINT [PK_tblConcessionAccount] PRIMARY KEY CLUSTERED ([pkConcessionAccountId] ASC),
    CONSTRAINT [FK_tblConcessionAccount_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

