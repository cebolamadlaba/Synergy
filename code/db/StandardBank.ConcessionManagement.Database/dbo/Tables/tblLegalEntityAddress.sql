CREATE TABLE [dbo].[tblLegalEntityAddress] (
    [pkLegalEntityAddressId] INT           IDENTITY (1, 1) NOT NULL,
    [fkLegalEntityId]        INT           NOT NULL,
    [ContactPerson]          VARCHAR (150) NULL,
    [CustomerName]           VARCHAR (200) NOT NULL,
    [PostalAddress]          VARCHAR (250) NULL,
    [City]                   VARCHAR (150) NULL,
    [PostalCode]             VARCHAR (50)  NULL,
    [DateCreated]            DATETIME      NOT NULL,
    [Datemodified]           DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([pkLegalEntityAddressId] ASC),
    FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId])
);

