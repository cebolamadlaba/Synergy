CREATE TABLE [dbo].[tblLegalEntityBOLUser] (
    [pkLegalEntityBOLUserId] INT           IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityAccountId] INT           NULL,
    [BOLUserId]              VARCHAR (250) NULL,
    CONSTRAINT [PK_tblLegalEntityBOLUser] PRIMARY KEY CLUSTERED ([pkLegalEntityBOLUserId] ASC),
    CONSTRAINT [FK_tblLegalEntityBOLUser_tblLegalEntity] FOREIGN KEY ([fkLegalEntityAccountId]) REFERENCES [dbo].[tblLegalEntityAccount] ([pkLegalEntityAccountId])
);

