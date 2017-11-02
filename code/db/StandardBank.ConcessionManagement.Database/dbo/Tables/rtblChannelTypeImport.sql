CREATE TABLE [dbo].[rtblChannelTypeImport] (
    [pkChannelTypeImportId] INT          IDENTITY (1, 1) NOT NULL,
    [fkChannelTypeId]       INT          NOT NULL,
    [ImportFileChannel]     VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_rtblChannelTypeImport] PRIMARY KEY CLUSTERED ([pkChannelTypeImportId] ASC),
    CONSTRAINT [FK_rtblChannelTypeImport_rtblChannelType] FOREIGN KEY ([fkChannelTypeId]) REFERENCES [dbo].[rtblChannelType] ([pkChannelTypeId])
);

