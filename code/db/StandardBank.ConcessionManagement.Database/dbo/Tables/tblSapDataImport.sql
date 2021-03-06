CREATE TABLE [dbo].[tblSapDataImport] (
    [PricepointId]       INT           NOT NULL,
    [CustomerId]         VARCHAR (50)  NULL,
    [AccountName]        VARCHAR (50)  NULL,
    [ProductId]          VARCHAR (50)  NULL,
    [Description]        VARCHAR (500) NULL,
    [GroupId]            VARCHAR (50)  NULL,
    [SubGroupId]         VARCHAR (50)  NULL,
    [BankIdentifierId]   VARCHAR (50)  NULL,
    [AccountNo]          VARCHAR (50)  NULL,
    [OptionId]           VARCHAR (50)  NULL,
    [UserId]             VARCHAR (50)  NULL,
    [TierFromValue]      VARCHAR (50)  NULL,
    [TierToValue]        VARCHAR (50)  NULL,
    [AdvaloremFee]       VARCHAR (50)  NULL,
    [MinimumFee]         VARCHAR (50)  NULL,
    [MaximumFee]         VARCHAR (50)  NULL,
    [FlatFee]            VARCHAR (50)  NULL,
    [CommunicationFee]   VARCHAR (50)  NULL,
    [TableNo]            VARCHAR (50)  NULL,
    [TransactionVolume]  VARCHAR (50)  NULL,
    [TransactionRevenue] VARCHAR (50)  NULL,
    [ProductName]        VARCHAR (50)  NULL,
    [Channel]            VARCHAR (50)  NULL,
    [MarketSegment]      VARCHAR (50)  NULL,
    [SequenceId]         VARCHAR (50)  NULL,
    [EntryDate]          VARCHAR (50)  NULL,
    [EffectiveDate]      VARCHAR (50)  NULL,
    [ExpiryDate]         VARCHAR (50)  NULL,
    [TerminationDate]    VARCHAR (50)  NULL,
    [Status]             VARCHAR (50)  NULL,
    [ImportDate]         DATETIME      CONSTRAINT [DF_tblSapDataImport_ImportDate] DEFAULT (getdate()) NOT NULL,
    [LastUpdatedDate]    DATETIME      NULL,
    [ExportRow]          BIT           CONSTRAINT [DF_tblSapDataImport_ExportRow] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblSapDataImport] PRIMARY KEY CLUSTERED ([PricepointId] ASC)
);








GO



GO


