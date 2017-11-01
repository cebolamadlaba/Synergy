CREATE TABLE [dbo].[tblSapDataImportConfiguration] (
    [pkSapDataImportConfigurationId] INT           IDENTITY (1, 1) NOT NULL,
    [FileImportLocation]             VARCHAR (500) NOT NULL,
    [FileExportLocation]             VARCHAR (500) NOT NULL,
    [SupportEmailAddress]            VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_tblSapDataImportConfiguration] PRIMARY KEY CLUSTERED ([pkSapDataImportConfigurationId] ASC)
);

