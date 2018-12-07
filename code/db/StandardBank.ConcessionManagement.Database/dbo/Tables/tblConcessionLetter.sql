CREATE TABLE [dbo].[tblConcessionLetter] (
    [pkConcessionLetter]   INT           IDENTITY (1, 1) NOT NULL,
    [fkConcessionDetailId] INT           NULL,
    [Location]             VARCHAR (MAX) NULL,
    CONSTRAINT [PK_tblConcessionLetter] PRIMARY KEY CLUSTERED ([pkConcessionLetter] ASC)
);

